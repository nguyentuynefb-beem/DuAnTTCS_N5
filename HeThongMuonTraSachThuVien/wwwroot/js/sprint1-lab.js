document.addEventListener("DOMContentLoaded", () => {
    const state = {
        loginAttempts: 0,
        lockedUntil: 0,
        editingUserIndex: null,
        sessionRole: "—",
        sessionStatus: "Chưa đăng nhập",
        users: [
            { name: "Nguyễn Văn A", email: "admin@thuvien.local", role: "Admin", status: "Active" },
            { name: "Trần Thị B", email: "thuthu@thuvien.local", role: "ThuThu", status: "Active" },
            { name: "Lê Văn C", email: "quanly@thuvien.local", role: "QuanLy", status: "Active" },
            { name: "Phạm Văn D", email: "bandoc@thuvien.local", role: "BanDoc", status: "Pending" }
        ],
        pendingReaders: [
            { id: "RG-1001", name: "Phạm Văn D", code: "SV2026001", email: "phamd@email.com", status: "Chờ duyệt", reason: "" }
        ],
        cards: [
            { id: "TV-0001", reader: "Phạm Văn D", type: "Sinh viên", expiry: "2027-01-01", status: "Hoạt động" },
            { id: "TV-0002", reader: "Nguyễn Minh E", type: "Giảng viên", expiry: "2025-12-31", status: "Hết hạn" }
        ],
        policies: [
            { type: "Sinh viên", maxBooks: 5, loanDays: 14, extendTimes: 1, dailyFine: 0, lostFine: 0, damageFine: 0, effectiveFrom: "2026-01-01" },
            { type: "Giảng viên", maxBooks: 7, loanDays: 21, extendTimes: 2, dailyFine: 0, lostFine: 0, damageFine: 0, effectiveFrom: "2026-01-01" }
        ],
        profile: {
            cardId: "TV-0001",
            cardType: "Sinh viên",
            cardExpiry: "2027-01-01",
            cardStatus: "Hoạt động",
            name: "Phạm Văn D",
            email: "bandoc@example.com",
            phone: "0909123456",
            address: "Quận 1, TP.HCM",
            currentPassword: "12345678",
            newPassword: "",
            confirmPassword: ""
        },
        resetLogs: [],
        authors: ["Nguyễn Nhật Ánh", "J.K. Rowling", "Stephen King"],
        genres: [
            { name: "Văn học", active: true },
            { name: "Kinh tế", active: true },
            { name: "Kỹ năng sống", active: true }
        ],
        shelves: [
            { warehouse: "KHO A", shelf: "A-01", active: true },
            { warehouse: "KHO A", shelf: "A-02", active: true },
            { warehouse: "KHO B", shelf: "B-01", active: false }
        ],
        closures: [
            { day: "Thứ 2", open: true },
            { day: "Thứ 3", open: true },
            { day: "Thứ 4", open: true },
            { day: "Thứ 5", open: true },
            { day: "Thứ 6", open: true },
            { day: "Thứ 7", open: false },
            { day: "Chủ nhật", open: false }
        ],
        dueDate: "2026-09-30",
        auditLogs: []
    };

    const $ = (id) => document.getElementById(id);

    const escapeHtml = (value) => String(value)
        .replaceAll("&", "&amp;")
        .replaceAll("<", "&lt;")
        .replaceAll(">", "&gt;")
        .replaceAll('"', "&quot;")
        .replaceAll("'", "&#39;");

    const todayStamp = () => {
        const d = new Date();
        const pad = (n) => String(n).padStart(2, "0");
        return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}`;
    };

    const toastHost = $("toastHost");
    function showToast(kind, title, message) {
        const cls = kind === "success" ? "text-bg-success"
            : kind === "danger" ? "text-bg-danger"
            : kind === "warning" ? "text-bg-warning"
            : "text-bg-primary";

        const toast = document.createElement("div");
        toast.className = `alert ${cls} shadow mb-2`;
        toast.style.minWidth = "320px";
        toast.innerHTML = `
            <div class="fw-semibold">${escapeHtml(title)}</div>
            <div class="small">${escapeHtml(message)}</div>
        `;
        toastHost.appendChild(toast);
        setTimeout(() => {
            toast.remove();
        }, 3500);
    }

    async function addAudit(action, target) {
        const actor = state.sessionRole === "—" ? "Hệ thống" : state.sessionRole;
        try {
            await fetch("/api/NhatKy", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    tenNguoiThucHien: actor,
                    hanhDong: action,
                    doiTuong: target
                })
            });
        } catch (err) {
            console.error("Không thể ghi nhật ký:", err);
        }
        renderAudit();
    }

    function renderLoginMeta() {
        $("loginAttemptsBadge").textContent = `Sai: ${state.loginAttempts}/5`;
        const remain = Math.max(0, Math.ceil((state.lockedUntil - Date.now()) / 60000));
        $("loginLockBadge").textContent = state.lockedUntil > Date.now() ? `Khoá: ${remain} phút` : "Khoá: 0 phút";
        $("loginRoleBadge").textContent = `Vai trò: ${state.sessionRole}`;
        $("sessionRoleText").textContent = state.sessionRole;
        $("lockStatusText").textContent = state.lockedUntil > Date.now() ? `Khoá tạm ${remain} phút` : "Không khoá";
        $("loginMessage").textContent = state.sessionStatus;
    }

function renderUsers(users) {
    const tbody = $("userTableBody");

    if (!users || users.length === 0) {
        tbody.innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-muted py-4">
                    Chưa có tài khoản.
                </td>
            </tr>
        `;
        return;
    }

    tbody.innerHTML = users.map((u) => {
        let roleText = u.vaiTro;

        if (u.vaiTro === "Admin") {
            roleText = "Quản trị hệ thống";
        } else if (u.vaiTro === "QuanLy") {
            roleText = "Quản lý thư viện";
        } else if (u.vaiTro === "ThuThu") {
            roleText = "Thủ thư";
        }

        const statusText = u.trangThai
            ? "Hoạt động"
            : "Không hoạt động";

        const statusClass = u.trangThai
            ? "bg-success"
            : "bg-secondary";

        return `
            <tr>
                <td>${escapeHtml(u.hoTen)}</td>
                <td>${escapeHtml(u.email)}</td>
                <td>
                    <span class="badge bg-info text-dark">
                        ${escapeHtml(roleText)}
                    </span>
                </td>
                <td>
                    <span class="badge ${statusClass}">
                        ${statusText}
                    </span>
                </td>
            </tr>
        `;
    }).join("");
}

    function startEditUser(index) {
        const u = state.users[index];
        if (!u) return;
        state.editingUserIndex = index;
        $("newUserName").value = u.name;
        $("newUserEmail").value = u.email;
        $("newUserPhone").value = u.phone || "";
        $("newUserRole").value = u.role;
        $("newUserStatus").value = u.status;
        $("btnAddUser").textContent = "Cập nhật tài khoản";
        $("btnCancelEditUser").classList.remove("d-none");
    }

    function cancelEditUser() {
        state.editingUserIndex = null;
        $("newUserName").value = "";
        $("newUserEmail").value = "";
        $("newUserPhone").value = "";
        $("newUserRole").value = "ThuThu";
        $("newUserStatus").value = "Active";
        $("btnAddUser").textContent = "Tạo tài khoản";
        $("btnCancelEditUser").classList.add("d-none");
    }

    function renderPendingReaders() {
        const tbody = $("pendingTableBody");
        const select = $("pendingSelect");

        if (state.pendingReaders.length === 0) {
            tbody.innerHTML = `<tr><td colspan="5" class="text-center text-muted py-4">Chưa có hồ sơ chờ duyệt</td></tr>`;
            select.innerHTML = `<option value="">Không có hồ sơ</option>`;
            return;
        }

        tbody.innerHTML = state.pendingReaders.map((r) => `
            <tr>
                <td>${escapeHtml(r.id)}</td>
                <td>${escapeHtml(r.name)}</td>
                <td>${escapeHtml(r.code)}</td>
                <td>${escapeHtml(r.email)}</td>
                <td><span class="badge bg-warning text-dark">${escapeHtml(r.status)}</span></td>
            </tr>
        `).join("");

        select.innerHTML = state.pendingReaders.map((r) => `<option value="${escapeHtml(r.id)}">${escapeHtml(r.name)} · ${escapeHtml(r.code)}</option>`).join("");
    }

    function renderCards() {
        const tbody = $("cardTableBody");
        tbody.innerHTML = state.cards.map((c) => `
            <tr>
                <td><code>${escapeHtml(c.id)}</code></td>
                <td>${escapeHtml(c.reader)}</td>
                <td>${escapeHtml(c.type)}</td>
                <td>${escapeHtml(c.expiry)}</td>
                <td><span class="badge ${c.status === "Hoạt động" ? "bg-success" : "bg-secondary"}">${escapeHtml(c.status)}</span></td>
            </tr>
        `).join("");
        $("profileCardId").value = state.profile.cardId;
        $("profileCardType").value = state.profile.cardType;
        $("profileCardExpiry").value = state.profile.cardExpiry;
        $("profileCardStatus").value = state.profile.cardStatus;
        $("profileName").value = state.profile.name;
        $("profileEmail").value = state.profile.email;
        $("profilePhone").value = state.profile.phone;
        $("profileAddress").value = state.profile.address;
    }

    function renderPolicies() {
        const tbody = $("policyTableBody");
        tbody.innerHTML = state.policies.map((p) => `
            <tr>
                <td>${escapeHtml(p.type)}</td>
                <td>${p.maxBooks}</td>
                <td>${p.loanDays}</td>
                <td>${p.extendTimes}</td>
                <td>${p.dailyFine}</td>
                <td>${escapeHtml(p.effectiveFrom)}</td>
            </tr>
        `).join("");
    }

    function renderResetLogs() {
        const area = $("resetLogArea");
        if (state.resetLogs.length === 0) {
            area.innerHTML = `<div class="alert alert-light border">Chưa có yêu cầu đặt lại mật khẩu.</div>`;
            return;
        }
        area.innerHTML = state.resetLogs.map((r) => `
            <div class="list-group-item border rounded-3 mb-2">
                <div class="fw-semibold">${escapeHtml(r.when)}</div>
                <div class="small text-muted">${escapeHtml(r.message)}</div>
            </div>
        `).join("");
    }

    function renderAuthorsGenres() {
        $("authorTags").innerHTML = state.authors.map((a) => `
            <span class="badge bg-secondary-subtle text-dark border border-secondary-subtle rounded-pill px-3 py-2">${escapeHtml(a)}</span>
        `).join("");

        $("genreTags").innerHTML = state.genres.map((g, idx) => `
            <button type="button"
                    class="btn btn-sm ${g.active ? "btn-success" : "btn-outline-secondary"} rounded-pill"
                    data-genre-index="${idx}">
                ${escapeHtml(g.name)}
                <small class="ms-1">${g.active ? "Active" : "Disabled"}</small>
            </button>
        `).join("");

        document.querySelectorAll("[data-genre-index]").forEach((btn) => {
            btn.addEventListener("click", () => {
                const index = Number(btn.getAttribute("data-genre-index"));
                state.genres[index].active = !state.genres[index].active;
                renderAuthorsGenres();
                addAudit("Sửa", `Thể loại ${state.genres[index].name}`);
            });
        });
    }

    function renderShelves() {
        $("shelfTableBody").innerHTML = state.shelves.map((s) => `
            <tr>
                <td>${escapeHtml(s.warehouse)}</td>
                <td><code>${escapeHtml(s.shelf)}</code></td>
                <td><span class="badge ${s.active ? "bg-success" : "bg-secondary"}">${s.active ? "Hoạt động" : "Ngừng"}</span></td>
            </tr>
        `).join("");

        $("closureList").innerHTML = state.closures.map((c, idx) => `
            <button type="button" class="list-group-item list-group-item-action d-flex justify-content-between align-items-center" data-closure-index="${idx}">
                <span>${escapeHtml(c.day)}</span>
                <span class="badge ${c.open ? "bg-success" : "bg-danger"}">${c.open ? "Mở cửa" : "Đóng cửa"}</span>
            </button>
        `).join("");

        document.querySelectorAll("[data-closure-index]").forEach((btn) => {
            btn.addEventListener("click", () => {
                const index = Number(btn.getAttribute("data-closure-index"));
                state.closures[index].open = !state.closures[index].open;
                renderShelves();
                addAudit("Sửa", `Lịch ${state.closures[index].day}`);
            });
        });
    }

    function formatAuditTime(iso) {
        const d = new Date(iso);
        const pad = (n) => String(n).padStart(2, "0");
        return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}`;
    }

    async function renderAudit() {
        const tbody = $("auditTableBody");

        // S1-10: Chỉ Quản trị hệ thống (Admin) được xem nhật ký hoạt động.
        if (state.sessionRole !== "Admin") {
            state.auditLogs = [];
            $("auditCount").textContent = "-";
            tbody.innerHTML = `
                <tr>
                    <td colspan="5" class="text-center text-muted py-4">
                        Chỉ Quản trị hệ thống (Admin) được phép xem nhật ký hoạt động.
                        Vui lòng đăng nhập bằng tài khoản Admin.
                    </td>
                </tr>
            `;
            return;
        }

        const actor = $("auditActorFilter").value.trim();
        const action = $("auditActionFilter").value;
        const from = $("auditFromFilter").value;
        const to = $("auditToFilter").value;

        const params = new URLSearchParams();
        if (actor) params.set("nguoiThucHien", actor);
        if (action) params.set("hanhDong", action);
        if (from) params.set("tuNgay", from);
        if (to) params.set("denNgay", to);

        let filtered = [];
        try {
            const res = await fetch(`/api/NhatKy?${params.toString()}`, {
                headers: { "X-Actor-Role": state.sessionRole }
            });

            if (res.status === 403) {
                $("auditCount").textContent = "-";
                tbody.innerHTML = `
                    <tr>
                        <td colspan="5" class="text-center text-muted py-4">
                            Bạn không có quyền xem nhật ký hoạt động.
                        </td>
                    </tr>
                `;
                return;
            }

            if (res.ok) {
                filtered = await res.json();
            }
        } catch (err) {
            console.error("Không thể tải nhật ký:", err);
        }

        state.auditLogs = filtered;

        $("auditCount").textContent = filtered.length;
        tbody.innerHTML = filtered.map((l) => `
            <tr>
                <td>${escapeHtml(formatAuditTime(l.thoiGian))}</td>
                <td>${escapeHtml(l.tenNguoiThucHien)}</td>
                <td>${escapeHtml(l.hanhDong)}</td>
                <td>${escapeHtml(l.doiTuong ?? "")}</td>
                <td>${escapeHtml(l.diaChiIP ?? "")}</td>
            </tr>
        `).join("");
    }

    function populateProfile() {
        $("profileCardId").value = state.profile.cardId;
        $("profileCardType").value = state.profile.cardType;
        $("profileCardExpiry").value = state.profile.cardExpiry;
        $("profileCardStatus").value = state.profile.cardStatus;
        $("profileName").value = state.profile.name;
        $("profileEmail").value = state.profile.email;
        $("profilePhone").value = state.profile.phone;
        $("profileAddress").value = state.profile.address;
    }

    async function loadVaiTro() {
    try {
        const response = await fetch("/Sprint1Lab/GetVaiTro");

        if (!response.ok) {
            throw new Error("Không thể tải danh sách vai trò.");
        }

        const vaiTros = await response.json();
        const select = $("newUserRole");

        select.innerHTML = `
            <option value="">-- Chọn vai trò --</option>
        `;

        vaiTros.forEach((vaiTro) => {
            let tenHienThi = vaiTro.ten;

            if (vaiTro.ten === "Admin") {
                tenHienThi = "Quản trị hệ thống";
            } else if (vaiTro.ten === "QuanLy") {
                tenHienThi = "Quản lý thư viện";
            } else if (vaiTro.ten === "ThuThu") {
                tenHienThi = "Thủ thư";
            }

            select.innerHTML += `
                <option value="${vaiTro.id}">
                    ${escapeHtml(tenHienThi)}
                </option>
            `;
        });
    } catch (error) {
        console.error(error);

        showToast(
            "danger",
            "Lỗi",
            "Không thể tải danh sách vai trò."
        );
    }
}

    async function loadTaiKhoan() {
    try {
        const response = await fetch("/Sprint1Lab/GetTaiKhoan");

        if (!response.ok) {
            throw new Error("Không thể tải danh sách tài khoản.");
        }

        const users = await response.json();

        renderUsers(users);
    } catch (error) {
        console.error(error);

        $("userTableBody").innerHTML = `
            <tr>
                <td colspan="4" class="text-center text-danger py-4">
                    Không thể tải danh sách tài khoản.
                </td>
            </tr>
        `;

        showToast(
            "danger",
            "Lỗi",
            "Không thể tải danh sách tài khoản từ máy chủ."
        );
    }
}

function renderAll() {
    renderLoginMeta();
    renderPendingReaders();
    renderCards();
    renderPolicies();
    renderResetLogs();
    renderAuthorsGenres();
    renderShelves();
    renderAudit();
    populateProfile();
    $("dueDateInput").value = state.dueDate;

    loadVaiTro();
    loadTaiKhoan();
}

    $("btnLogin").addEventListener("click", () => {
        const email = $("loginEmail").value.trim();
        const password = $("loginPassword").value.trim();

        if (Date.now() < state.lockedUntil) {
            state.sessionStatus = "Tài khoản đang bị khoá tạm thời.";
            renderLoginMeta();
            showToast("warning", "Khoá tạm", "Đợi hết thời gian khoá rồi thử lại.");
            return;
        }

        const isValid = email.includes("@") && password.length >= 6;
        if (!isValid) {
            state.loginAttempts += 1;
            state.sessionStatus = `Sai thông tin (${state.loginAttempts}/5)`;

            if (state.loginAttempts >= 5) {
                state.loginAttempts = 0;
                state.lockedUntil = Date.now() + 15 * 60 * 1000;
                state.sessionStatus = "Sai 5 lần liên tiếp. Đã khoá 15 phút.";
                addAudit("Đăng nhập thất bại", email || "Không xác định");
                showToast("danger", "Khoá tạm 15 phút", "Đăng nhập sai 5 lần liên tiếp.");
            } else {
                addAudit("Đăng nhập thất bại", email || "Không xác định");
                showToast("warning", "Đăng nhập thất bại", "Thông tin chưa hợp lệ.");
            }

            renderLoginMeta();
            return;
        }

        state.loginAttempts = 0;
        state.lockedUntil = 0;
        if (email.startsWith("admin")) state.sessionRole = "Admin";
        else if (email.startsWith("quanly")) state.sessionRole = "QuanLy";
        else if (email.startsWith("thuthu")) state.sessionRole = "ThuThu";
        else state.sessionRole = "BanDoc";

        state.sessionStatus = "Đăng nhập thành công.";
        addAudit("Đăng nhập", email);
        renderLoginMeta();
        showToast("success", "Đăng nhập thành công", `Vai trò mô phỏng: ${state.sessionRole}`);
    });

    $("btnLogout").addEventListener("click", () => {
        state.sessionRole = "—";
        state.sessionStatus = "Đã đăng xuất.";
        addAudit("Đăng xuất", $("loginEmail").value.trim() || "Không xác định");
        renderLoginMeta();
        showToast("info", "Đã đăng xuất", "Phiên mô phỏng đã được reset.");
    });

    $("btnResetLogin").addEventListener("click", () => {
        state.loginAttempts = 0;
        state.lockedUntil = 0;
        state.sessionRole = "—";
        state.sessionStatus = "Đã reset trạng thái đăng nhập.";
        renderLoginMeta();
        showToast("secondary", "Đã reset", "Trạng thái đăng nhập đã được xoá.");
    });

$("btnAddUser").addEventListener("click", async () => {
    const hoTen = $("newUserName").value.trim();
    const email = $("newUserEmail").value.trim();
    const soDienThoai = $("newUserPhone").value.trim();
    const vaiTroId = $("newUserRole").value;
    const trangThai = $("newUserStatus").value === "true";

    // Kiểm tra họ tên
    if (!hoTen) {
        showToast(
            "warning",
            "Thiếu thông tin",
            "Vui lòng nhập họ tên."
        );
        $("newUserName").focus();
        return;
    }

    // Kiểm tra email
    if (!email) {
        showToast(
            "warning",
            "Thiếu thông tin",
            "Vui lòng nhập email."
        );
        $("newUserEmail").focus();
        return;
    }

    const emailRegex =
        /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailRegex.test(email)) {
        showToast(
            "warning",
            "Email không hợp lệ",
            "Vui lòng nhập đúng định dạng email."
        );
        $("newUserEmail").focus();
        return;
    }

    // Kiểm tra vai trò
    if (!vaiTroId) {
        showToast(
            "warning",
            "Thiếu thông tin",
            "Vui lòng chọn vai trò."
        );
        $("newUserRole").focus();
        return;
    }

    const requestData = {
        HoTen: hoTen,
        Email: email,
        SoDienThoai: soDienThoai || null,
        VaiTroId: Number(vaiTroId),
        TrangThai: trangThai
    };

    try {
        const response = await fetch("/Sprint1Lab/TaoTaiKhoan", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(requestData)
        });

        const result = await response.json();

        if (!response.ok) {
            if (result.errors) {
                const messages = Object.values(result.errors)
                    .flat()
                    .filter(Boolean);

                showToast(
                    "danger",
                    "Không thể tạo tài khoản",
                    messages.join(" ")
                );
            } else {
                showToast(
                    "danger",
                    "Không thể tạo tài khoản",
                    result.message || "Có lỗi xảy ra."
                );
            }

            return;
        }

        showToast(
            "success",
            "Thành công",
            result.message || "Tạo tài khoản thành công."
        );

        // Xóa dữ liệu form
        $("newUserName").value = "";
        $("newUserEmail").value = "";
        $("newUserPhone").value = "";
        $("newUserRole").value = "";

        // Tải lại danh sách từ database
        await loadTaiKhoan();

    } catch (error) {
        console.error(error);

        showToast(
            "danger",
            "Lỗi kết nối",
            "Không thể kết nối đến máy chủ."
        );
    }
});

    $("btnCancelEditUser").addEventListener("click", () => {
        cancelEditUser();
    });
    $("btnSignup").addEventListener("click", () => {
        const name = $("signupName").value.trim();
        const dob = $("signupDob").value;
        const code = $("signupCode").value.trim();
        const email = $("signupEmail").value.trim();
        const phone = $("signupPhone").value.trim();
        const password = $("signupPassword").value.trim();
        const password2 = $("signupPassword2").value.trim();

        if (!name || !email.includes("@") || password.length < 8 || password !== password2) {
            showToast("warning", "Dữ liệu chưa hợp lệ", "Kiểm tra tên, email và mật khẩu (>= 8 ký tự).");
            return;
        }

        const row = {
            id: `RG-${String(Date.now()).slice(-6)}`,
            name,
            dob,
            code,
            email,
            phone,
            status: "Chờ duyệt",
            reason: ""
        };

        state.pendingReaders.unshift(row);
        $("signupName").value = "";
        $("signupDob").value = "";
        $("signupCode").value = "";
        $("signupEmail").value = "";
        $("signupPhone").value = "";
        $("signupPassword").value = "";
        $("signupPassword2").value = "";

        renderPendingReaders();
        addAudit("Thêm", `Đăng ký ${email}`);
        showToast("success", "Đã tạo hồ sơ chờ duyệt", "Bạn đọc vừa đăng ký đã vào danh sách chờ.");
    });

    $("btnApproveCard").addEventListener("click", () => {
        const selectedId = $("pendingSelect").value;
        const reader = state.pendingReaders.find(x => x.id === selectedId);

        if (!reader) {
            showToast("warning", "Chưa có hồ sơ", "Không có hồ sơ để duyệt.");
            return;
        }

        const cardType = $("cardTypeSelect").value;
        const expiry = $("cardExpiry").value || "2027-01-01";
        const cardNo = `TV-${String(state.cards.length + 1).padStart(4, "0")}`;

        state.cards.unshift({
            id: cardNo,
            reader: reader.name,
            type: cardType,
            expiry,
            status: "Hoạt động"
        });

        state.pendingReaders = state.pendingReaders.filter(x => x.id !== selectedId);
        renderPendingReaders();
        renderCards();
        addAudit("Cấp thẻ", `Thẻ ${cardNo} - Bạn đọc ${reader.name}`);
        showToast("success", "Đã cấp thẻ", `Sinh mã thẻ ${cardNo}`);
    });

    $("btnRejectCard").addEventListener("click", () => {
        const selectedId = $("pendingSelect").value;
        const reason = $("rejectReason").value.trim();
        const reader = state.pendingReaders.find(x => x.id === selectedId);

        if (!reader) {
            showToast("warning", "Chưa có hồ sơ", "Không có hồ sơ để từ chối.");
            return;
        }

        reader.status = "Từ chối";
        reader.reason = reason || "Không nêu lý do";
        state.pendingReaders = state.pendingReaders.filter(x => x.id !== selectedId);
        renderPendingReaders();
        addAudit("Sửa", `Từ chối ${reader.email}`);
        showToast("danger", "Đã từ chối hồ sơ", reader.reason);
    });

    $("btnAddPolicy").addEventListener("click", () => {
        const item = {
            type: $("policyCardType").value,
            maxBooks: Number($("policyMaxBooks").value || 0),
            loanDays: Number($("policyLoanDays").value || 0),
            extendTimes: Number($("policyExtendTimes").value || 0),
            dailyFine: Number($("policyDailyFine").value || 0),
            lostFine: Number($("policyLostFine").value || 0),
            damageFine: Number($("policyDamageFine").value || 0),
            effectiveFrom: $("policyEffectiveFrom").value || "2026-09-23"
        };

        if (item.maxBooks <= 0 || item.loanDays <= 0) {
            showToast("warning", "Dữ liệu không hợp lệ", "Số sách và số ngày phải lớn hơn 0.");
            return;
        }

        state.policies.unshift(item);
        renderPolicies();
        addAudit("Sửa chính sách mượn", `Chính sách mượn - Loại thẻ ${item.type}`);
        showToast("success", "Đã lưu chính sách", "Bản ghi mới đã được thêm vào đầu danh sách.");
    });

    $("btnSaveProfile").addEventListener("click", () => {
        state.profile.email = $("profileEmail").value.trim();
        state.profile.phone = $("profilePhone").value.trim();
        state.profile.address = $("profileAddress").value.trim();
        renderCards();
        addAudit("Sửa", `Hồ sơ ${state.profile.email}`);
        showToast("success", "Đã lưu hồ sơ", "Thông tin liên hệ đã được cập nhật ở màn test.");
    });

    $("btnChangePassword").addEventListener("click", () => {
        const current = $("currentPassword").value.trim();
        const next = $("newPassword").value.trim();
        const confirm = $("confirmPassword").value.trim();

        if (current !== state.profile.currentPassword) {
            showToast("warning", "Sai mật khẩu hiện tại", "Không khớp với mật khẩu mô phỏng.");
            return;
        }

        if (next.length < 8 || next !== confirm) {
            showToast("warning", "Mật khẩu chưa hợp lệ", "Mật khẩu mới phải đủ dài và trùng khớp.");
            return;
        }

        state.profile.currentPassword = next;
        $("currentPassword").value = "";
        $("newPassword").value = "";
        $("confirmPassword").value = "";
        addAudit("Sửa", `Đổi mật khẩu ${state.profile.email}`);
        showToast("success", "Đổi mật khẩu thành công", "Mật khẩu mô phỏng đã được thay đổi.");
    });

    $("btnResetPassword").addEventListener("click", () => {
        const email = $("resetEmail").value.trim();
        const msg = `Đã gửi link đặt lại mật khẩu tới ${email}. Link hiệu lực 30 phút và dùng một lần (mô phỏng).`;
        state.resetLogs.unshift({ when: todayStamp(), message: msg });
        renderResetLogs();
        addAudit("Khác", `Reset mật khẩu ${email}`);
        showToast("info", "Đã gửi link", msg);
    });

    $("btnAddAuthor").addEventListener("click", () => {
        const value = $("authorInput").value.trim();
        if (!value) return;
        if (state.authors.some(x => x.toLowerCase() === value.toLowerCase())) {
            showToast("warning", "Trùng tác giả", "Tác giả này đã có trong danh mục.");
            return;
        }
        state.authors.unshift(value);
        $("authorInput").value = "";
        renderAuthorsGenres();
        addAudit("Thêm", `Tác giả ${value}`);
        showToast("success", "Đã thêm tác giả", value);
    });

    $("btnAddGenre").addEventListener("click", () => {
        const value = $("genreInput").value.trim();
        if (!value) return;
        if (state.genres.some(x => x.name.toLowerCase() === value.toLowerCase())) {
            showToast("warning", "Trùng thể loại", "Thể loại này đã có trong danh mục.");
            return;
        }
        state.genres.unshift({ name: value, active: true });
        $("genreInput").value = "";
        renderAuthorsGenres();
        addAudit("Thêm", `Thể loại ${value}`);
        showToast("success", "Đã thêm thể loại", value);
    });

    $("btnAddShelf").addEventListener("click", () => {
        const warehouse = $("warehouseInput").value;
        const shelf = $("shelfInput").value.trim();
        if (!shelf) return;

        if (state.shelves.some(x => x.warehouse === warehouse && x.shelf.toLowerCase() === shelf.toLowerCase())) {
            showToast("warning", "Mã kệ trùng", "Mã kệ đã tồn tại trong kho này.");
            return;
        }

        state.shelves.unshift({ warehouse, shelf, active: true });
        $("shelfInput").value = "";
        renderShelves();
        addAudit("Thêm", `Kệ ${warehouse}-${shelf}`);
        showToast("success", "Đã thêm kệ", `${warehouse} / ${shelf}`);
    });

    $("btnMoveDueDate").addEventListener("click", () => {
        const current = $("dueDateInput").value || state.dueDate;
        const d = new Date(`${current}T00:00:00`);
        d.setDate(d.getDate() + 1);
        const next = d.toISOString().slice(0, 10);
        state.dueDate = next;
        $("dueDateInput").value = next;
        addAudit("Sửa", "Hạn trả mô phỏng");
        showToast("info", "Đã dời hạn trả", `Hạn trả chuyển sang ${next} (mô phỏng).`);
    });

    $("btnFilterAudit").addEventListener("click", () => renderAudit());

    $("btnResetAudit").addEventListener("click", () => {
        $("auditActorFilter").value = "";
        $("auditActionFilter").value = "";
        $("auditFromFilter").value = "";
        $("auditToFilter").value = "";
        renderAudit();
        showToast("secondary", "Đã reset bộ lọc", "Bộ lọc nhật ký đã được xoá.");
    });

    $("btnAddAuditSample").addEventListener("click", async () => {
        const samples = [
            { tenNguoiThucHien: "admin@thuvien.local", hanhDong: "Đăng nhập", doiTuong: "Hệ thống" },
            { tenNguoiThucHien: "thuthu@thuvien.local", hanhDong: "Thêm", doiTuong: "Cấp thẻ" }
        ];
        try {
            await Promise.all(samples.map((s) => fetch("/api/NhatKy", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(s)
            })));
            showToast("success", "Đã sinh log mẫu", "Thêm 2 dòng nhật ký vào hệ thống.");
        } catch (err) {
            showToast("danger", "Lỗi", "Không thể ghi nhật ký mẫu.");
        }
        renderAudit();
    });

    ["auditActorFilter", "auditActionFilter", "auditFromFilter", "auditToFilter"].forEach((id) => {
        $(id).addEventListener("input", () => renderAudit());
        $(id).addEventListener("change", () => renderAudit());
    });

    renderAll();
});
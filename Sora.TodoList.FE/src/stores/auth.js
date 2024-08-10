import { defineStore } from "pinia";
import { reactive, ref } from "vue";
import UserAPI from "@/apis/user/UserAPI";
import CommonFunction from "@/commons/CommonFunction";

const LOCAL_STORAGE_KEY = "auth";

export const useAuthStore = defineStore("auth", () => {
    const loading = ref(false);
    const auth = reactive({
        accessToken: null,
        expiresAt: null,
        refreshToken: null,
    });

    /**
     * action cập nhật accessToken
     */
    async function handleLogin({ email, password }) {
        loading.value = true;
        const loginRes = await UserAPI.login({
            email,
            password,
        });

        if (!loginRes.success) {
            // xử lý lỗi
            return;
        }

        auth.accessToken = loginRes.data.accessToken;
        auth.expiresAt = CommonFunction.initDate(new Date())
            .add(loginRes.data.expiresIn, "second")
            .toDate();
        auth.refreshToken = loginRes.data.refreshToken;

        CommonFunction.setLocalStorage(LOCAL_STORAGE_KEY, auth);
        loading.value = false;
    }

    async function updateAccessToken({ accessToken, expiresIn, refreshToken }) {
        auth.accessToken = accessToken;
        auth.expiresAt = CommonFunction.initDate(new Date())
            .add(expiresIn, "second")
            .toDate();
        auth.refreshToken = refreshToken;

        CommonFunction.setLocalStorage(LOCAL_STORAGE_KEY, auth);
    }

    /**
     * Đồng bộ auth
     */
    function sync() {
        const loginResStr = CommonFunction.getLocalStorage(LOCAL_STORAGE_KEY);
        if (!loginResStr) {
            return;
        }
        const loginRes = JSON.parse(loginResStr);
        auth.accessToken = loginRes.accessToken;
        auth.expiresAt = CommonFunction.initDate(loginRes.expiresAt).toDate();
        auth.refreshToken = loginRes.refreshToken;
    }

    /**
     * Kiểm tra login
     */
    function hasLoggedIn() {
        return (
            auth.accessToken && auth.expiresAt && auth.expiresAt > new Date()
        );
    }

    /**
     * Đăng xuất
     */
    function logout() {
        auth.accessToken = null;
        auth.expiresAt = null;
        CommonFunction.removeLocalStorage(LOCAL_STORAGE_KEY);
    }

    return {
        loading,
        auth,
        handleLogin,
        updateAccessToken,
        sync,
        hasLoggedIn,
        logout,
    };
});

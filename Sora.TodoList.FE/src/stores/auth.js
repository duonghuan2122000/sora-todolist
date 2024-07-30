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
    });

    /**
     * action cập nhật accessToken
     */
    async function updateAccessToken({ email, password }) {
        loading.value = true;
        const loginRes = await UserAPI.login({
            email,
            password,
        });

        auth.accessToken = loginRes.data.accessToken;
        auth.expiresAt = CommonFunction.initDate(new Date())
            .add(loginRes.data.expiresIn, "second")
            .toDate();

        CommonFunction.setLocalStorage(LOCAL_STORAGE_KEY, auth);
        loading.value = false;
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

    return { loading, auth, updateAccessToken, sync, hasLoggedIn, logout };
});

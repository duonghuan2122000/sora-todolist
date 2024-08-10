import { defineStore } from "pinia";
import { reactive } from "vue";
import UserAPI from "@/apis/user/UserAPI";
export const useRegisterStore = defineStore("register", () => {
    const registerUser = reactive({
        email: "",
        password: "",
        confirmPassword: "",
    });

    async function submit() {
        const res = await UserAPI.register({
            email: registerUser.email,
            password: registerUser.password,
        });

        return res;
    }

    return { registerUser, submit };
});

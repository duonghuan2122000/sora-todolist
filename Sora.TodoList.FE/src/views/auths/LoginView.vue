<!-- 
    Trang đăng nhập
-->
<template>
    <div class="LoginView">
        <Card>
            <template #title> Đăng nhập </template>
            <template #content>
                <hr />
                <div class="InputGroup">
                    <label for="EmailField">Email</label>
                    <InputText
                        id="EmailField"
                        v-model="loginUser.email"
                        autofocus
                    />
                </div>
                <div class="InputGroup PasswordGroup">
                    <label for="PasswordField">Mật khẩu</label>
                    <Password
                        id="PasswordField"
                        v-model="loginUser.password"
                        :feedback="false"
                    />
                </div>
                <Button
                    type="button"
                    label="Đăng nhập"
                    class="SubmitBtn"
                    :loading="AuthStore.loading"
                    @click="handleLogin"
                />
                <div class="RegisterBox">
                    <div>Chưa có tài khoản?</div>
                    <Button
                        text
                        type="button"
                        label="Đăng ký"
                        @click="gotoRegisterPage"
                    />
                </div>
            </template>
        </Card>
    </div>
</template>

<script setup>
import { onMounted, reactive } from "vue";
import { useAuthStore } from "@/stores/auth";
import { useRouter } from "vue-router";

//#region Khởi tạo

const router = useRouter();

const loginUser = reactive({
    email: "",
    password: "",
});

const AuthStore = useAuthStore();

//#endregion

//#region Hook
onMounted(() => {});
//#endregion

//#region Hàm
/**
 * Hàm login
 */
const handleLogin = async () => {
    await AuthStore.handleLogin(loginUser);
    router.push({ name: "Home" });
};

/**
 * Hàm chuyển trang đăng ký
 */
const gotoRegisterPage = () => {
    router.push({ name: "Register" });
};
//#endregion
</script>

<style lang="scss" scoped>
.LoginView {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    .InputGroup {
        display: flex;
        flex-direction: column;
        gap: 8px;

        &.PasswordGroup {
            margin-top: 16px;
        }
    }

    .SubmitBtn {
        margin-top: 16px;
    }

    .RegisterBox {
        display: flex;
        flex-direction: row;
        justify-content: center;
        margin-top: 16px;

        & > div {
            display: flex;
            flex-direction: row;
            align-items: center;
            margin-right: 4px;
        }
    }
}

@media screen and (min-width: 768px) {
    .LoginView {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
}
</style>

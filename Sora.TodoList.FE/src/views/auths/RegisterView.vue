<!-- 
 Trang đăng ký
-->
<template>
    <div class="RegisterView">
        <Card>
            <template #title> Đăng ký </template>
            <template #content>
                <hr />
                <div class="InputGroup">
                    <label for="EmailField">Email</label>
                    <InputText
                        id="EmailField"
                        v-model="RegisterStore.registerUser.email"
                        autofocus
                    />
                </div>
                <div class="InputGroup">
                    <label for="PasswordField">Mật khẩu</label>
                    <Password
                        id="PasswordField"
                        v-model="RegisterStore.registerUser.password"
                        :feedback="false"
                    />
                </div>
                <div class="InputGroup">
                    <label for="ConfirmPasswordField">Xác nhận mật khẩu</label>
                    <Password
                        id="ConfirmPasswordField"
                        v-model="RegisterStore.registerUser.confirmPassword"
                        :feedback="false"
                    />
                </div>
                <Button
                    type="button"
                    label="Đăng ký"
                    class="SubmitBtn"
                    :loading="false"
                    @click="handleRegister"
                />
            </template>
        </Card>
    </div>
</template>

<script setup>
import { useRegisterStore } from "@/stores/register";
import { useAuthStore } from "@/stores/auth";
import { useRouter } from "vue-router";
//#region Khởi tạo
const RegisterStore = useRegisterStore();
const AuthStore = useAuthStore();
const router = useRouter();
//#endregion

//#region Hàm
/**
 * Xử lý đăng ký
 */
const handleRegister = async () => {
    const res = await RegisterStore.submit();
    if (res.success) {
        AuthStore.updateAccessToken({
            accessToken: res.data.accessToken,
            expiresIn: res.data.expiresIn,
            refreshToken: res.data.refreshToken,
        });
        router.push({ name: "Home" });
    }
};
//#endregion
</script>

<style lang="scss" scoped>
.RegisterView {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    .InputGroup {
        margin-top: 16px;
        display: flex;
        flex-direction: column;
        gap: 8px;
    }

    .SubmitBtn {
        margin-top: 16px;
    }
}
</style>

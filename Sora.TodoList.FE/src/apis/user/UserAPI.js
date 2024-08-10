// UserAPI
import HttpClientBase from "@/apis/base/HttpClient";
class UserAPI extends HttpClientBase {
    /**
     * Hàm login
     */
    async login({ email, password }) {
        const me = this;
        return await me.requestAsync({
            method: "POST",
            url: `${me.getDomain("Api")}/User/Login`,
            data: {
                email,
                password,
            },
        });
    }

    /**
     * Hàm đăng ký
     */
    async register({ email, password }) {
        const me = this;
        return await me.requestAsync({
            method: "POST",
            url: `${me.getDomain("Api")}/User/Register`,
            data: {
                email,
                password,
            },
        });
    }
}

export default new UserAPI();

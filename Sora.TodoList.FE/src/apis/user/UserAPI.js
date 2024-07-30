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
            url: `${me.getDomain("Api")}/api/User/Login`,
            data: {
                email,
                password,
            },
        });
    }
}

export default new UserAPI();

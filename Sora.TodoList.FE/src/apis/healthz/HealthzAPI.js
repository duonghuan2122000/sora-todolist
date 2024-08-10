// HealthzAPI
import HttpClientBase from "@/apis/base/HttpClient";

class HealthzAPI extends HttpClientBase {
    /**
     * Hàm kiểm tra api
     */
    async ping() {
        const me = this;
        return await me.requestAsync({
            method: "GET",
            url: `${me.getDomain("Api")}/Healthz`,
        });
    }
}

export default new HealthzAPI();

import HttpClientBase from "@/apis/base/HttpClient";

class ConstAPI extends HttpClientBase {
    /**
     * Lấy danh sách const
     */
    async getListConst(constListKey) {
        const me = this;
        return await me.requestAsync({
            url: `${me.getDomain("Api")}/Consts/${constListKey}`,
            method: "GET",
        });
    }
}

export default new ConstAPI();

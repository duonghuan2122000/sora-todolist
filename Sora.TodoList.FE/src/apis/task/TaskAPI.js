import HttpClientBase from "@/apis/base/HttpClient";

class TaskAPI extends HttpClientBase {
    /**
     * Lấy danh sách công việc
     */
    async getList(payload) {
        const me = this;
        return await me.requestAsync({
            url: `${me.getDomain("Api")}/Tasks/List`,
            method: "POST",
            data: payload,
        });
    }
}

export default new TaskAPI();

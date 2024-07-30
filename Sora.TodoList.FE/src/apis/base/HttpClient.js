// Base HttpClient
import axios from "axios";
class HttpClientBase {
    async requestAsync(config) {
        const me = this;
        const result = {};
        try {
            // format header
            config.headers = config.headers ? config.headers : {};
            me._processHeader(config.headers);

            config.withCredentials = true;

            const res = await axios(config);
            const data = res.data;
            result.success = data.success ?? false;
            if (result.success) {
                result.data = data.data;
            } else {
                result.code = data.code;
                result.message = data.message;
            }
        } catch (error) {
            result.success = false;
            result.code = "999";
            result.message = "Có lỗi xảy ra";
        }
        return result;
    }

    _processHeader(headers) {
        headers["Content-Type"] = "application/json";
    }

    getDomain(name) {
        return window._apis[name];
    }
}

export default HttpClientBase;

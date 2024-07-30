import dayjs from "dayjs";
import "dayjs/locale/vi";
import utc from "dayjs/plugin/utc";
import timezone from "dayjs/plugin/timezone";

dayjs.extend(utc);
dayjs.extend(timezone);

class CommonFunction {
    /**
     * Hàm get localStorage key
     */
    getLocalStorageKey(key) {
        return `_sora_${key}`;
    }

    /**
     * Hàm get localStorage
     */
    getLocalStorage(key) {
        const me = this;
        return localStorage.getItem(me.getLocalStorageKey(key));
    }

    /**
     * Set LocalStorage
     * @param {*} key
     * @param {*} value
     */
    setLocalStorage(key, value) {
        const me = this;
        if (typeof value !== "string") {
            value = JSON.stringify(value);
        }

        localStorage.setItem(me.getLocalStorageKey(key), value);
    }

    /**
     * Xóa localStorage
     */
    removeLocalStorage(key) {
        const me = this;
        localStorage.removeItem(me.getLocalStorageKey(key));
    }

    /**
     * Khởi tạo date
     */
    initDate(date) {
        return dayjs(date);
    }
}

export default new CommonFunction();

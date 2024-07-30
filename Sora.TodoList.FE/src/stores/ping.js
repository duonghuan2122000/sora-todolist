import { defineStore } from "pinia";
import { ref } from "vue";
import HealthzAPI from "@/apis/healthz/HealthzAPI";

export const usePingStore = defineStore("ping", () => {
    const healthz = ref(false);

    async function check() {
        const res = await HealthzAPI.ping();
        healthz.value = res.success;
        return healthz.value;
    }

    return { healthz, check };
});

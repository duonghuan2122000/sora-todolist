<template>
    <div id="body">
        <Toast />
        <MainLayout />
    </div>
</template>

<script setup>
import MainLayout from "@/layouts/MainLayout.vue";
import { usePingStore } from "@/stores/ping";
import { onMounted } from "vue";
import { useToast } from "primevue/usetoast";

const PingStore = usePingStore();
const $toast = useToast();

onMounted(async () => {
    const healthz = await PingStore.check();
    if (!healthz) {
        $toast.add({
            severity: "error",
            summary: "Thông báo lỗi",
            detail: "Server gặp lỗi",
            life: 3000,
        });
    }
});
</script>

<style lang="scss" scoped>
#body {
    height: 100vh;
    width: 100vw;
    overflow: hidden;
    display: flex;
    flex-direction: column;
}
</style>

<style lang="scss">
body,
html {
    margin: 0;
    padding: 0;
}
* {
    box-sizing: border-box;
}

#body {
    .HeaderLayout {
        flex: 0;
    }
    .BodyLayout {
        flex: 1;
    }
}
</style>

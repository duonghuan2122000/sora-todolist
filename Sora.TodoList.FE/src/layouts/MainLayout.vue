<!-- 
    MainLayout
-->
<template>
    <HeaderLayout v-if="LayoutStore.isUsedLayout" />
    <RouterView />
</template>

<script setup>
import { ref } from "vue";
import HeaderLayout from "./HeaderLayout.vue";
import { useLayoutStore } from "@/stores/layout";

const LayoutStore = useLayoutStore();
</script>

<!-- 
    MainLayout
-->
<template>
    <HeaderLayout class="HeaderLayout" v-if="LayoutStore.isUsedLayout" />
    <RouterView class="BodyLayout" />
</template>

<script setup>
import HeaderLayout from "./HeaderLayout.vue";
import { useLayoutStore } from "@/stores/layout";

const LayoutStore = useLayoutStore();
</script>
import { defineStore } from "pinia";
import { ref } from "vue";

export const useLayoutStore = defineStore("layout", () => {
    const isUsedLayout = ref(true);

    function updateUsedLayout(usedLayout = true) {
        isUsedLayout.value = usedLayout;
    }

    return { isUsedLayout, updateUsedLayout };
});

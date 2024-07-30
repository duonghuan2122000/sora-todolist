<!-- 
    Header layout
-->
<template>
    <Menubar class="HeaderLayout" :model="menuBarItems">
        <template #start>
            <div class="Logo" @click="gotoHome">TodoList Sora</div>
        </template>
        <template #item="{ item, props, hasSubmenu, root }">
            <a v-ripple class="flex items-center" v-bind="props.action">
                <span :class="item.icon" />
                <span class="ml-2">{{ item.label }}</span>
                <Badge
                    v-if="item.badge"
                    :class="{ 'ml-auto': !root, 'ml-2': root }"
                    :value="item.badge"
                />
                <span
                    v-if="item.shortcut"
                    class="ml-auto border border-surface rounded bg-emphasis text-muted-color text-xs p-1"
                    >{{ item.shortcut }}</span
                >
                <i
                    v-if="hasSubmenu"
                    :class="[
                        'pi pi-angle-down',
                        {
                            'pi-angle-down ml-2': root,
                            'pi-angle-right ml-auto': !root,
                        },
                    ]"
                ></i>
            </a>
        </template>
        <template #end>
            <div class="HeaderLayoutEnd">
                <div class="AvatarMenu">
                    <Avatar
                        class="Avatar"
                        image="https://primefaces.org/cdn/primevue/images/avatar/amyelsner.png"
                        shape="circle"
                        @click="avatarMenuToggle"
                    />
                    <Menu
                        ref="avatarMenuRef"
                        id="overlay_menu"
                        :model="avatarMenuItems"
                        :popup="true"
                    />
                </div>
            </div>
        </template>
    </Menubar>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useAuthStore } from "@/stores/auth";

const router = useRouter();
const AuthStore = useAuthStore();

const avatarMenuRef = ref();
const avatarMenuItems = ref([
    {
        label: "Thông tin cá nhân",
    },
    {
        label: "Đăng xuất",
        command: () => {
            AuthStore.logout();
            router.push({ name: "Login" });
        },
    },
]);

const avatarMenuToggle = (event) => {
    avatarMenuRef.value.toggle(event);
};

const menuBarItems = ref([
    {
        label: "Trang chủ",
        icon: "pi pi-home",
        command: () => {
            gotoHome();
        },
    },
]);

const gotoHome = () => {
    router.push({ name: "Home" });
};
</script>

<style lang="scss" scoped>
.HeaderLayout {
    .Logo {
        font-weight: 600;
        cursor: pointer;
    }
    .HeaderLayoutEnd {
        display: flex;
        flex-direction: row;
        align-items: center;

        .Avatar {
            cursor: pointer;
        }
    }
}

@media screen and (min-width: 768px) {
    .HeaderLayout {
        padding-left: 32px;
        padding-right: 32px;
    }
}
</style>

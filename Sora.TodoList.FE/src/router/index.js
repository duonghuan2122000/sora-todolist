import { createRouter, createWebHistory } from "vue-router";
import { useAuthStore } from "@/stores/auth";
import { useLayoutStore } from "@/stores/layout";
const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: "/",
            name: "Home",
            component: () => import("@/views/HomeView.vue"),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: "/tasks",
            name: "Tasks",
            component: () => import("@/views/tasks/ListView.vue"),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: "/task",
            name: "CreateTask",
            component: () => import("@/views/tasks/CreateView.vue"),
            meta: {
                requiresAuth: true,
            },
        },
        {
            path: "/login",
            name: "Login",
            component: () => import("@/views/auths/LoginView.vue"),
            meta: {
                usedLayout: false,
            },
        },
        {
            path: "/register",
            name: "Register",
            component: () => import("@/views/auths/RegisterView.vue"),
            meta: {
                usedLayout: false,
            },
        },
    ],
});

router.beforeEach((to, _, next) => {
    const AuthStore = useAuthStore();
    AuthStore.sync();

    const LayoutStore = useLayoutStore();
    if (to.meta.hasOwnProperty("usedLayout") && !to.meta.usedLayout) {
        LayoutStore.updateUsedLayout(false);
    } else {
        LayoutStore.updateUsedLayout(true);
    }

    if (to.meta.requiresAuth && !AuthStore.hasLoggedIn()) {
        next({ name: "Login" });
        return;
    }

    next();
});

export default router;

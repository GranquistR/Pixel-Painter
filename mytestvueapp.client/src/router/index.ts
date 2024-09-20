import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "HelloWorld",
      component: () => import("../components/HelloWorld.vue"),
    },
    {
      path: "/paint",
      name: "Painter View",
      component: () => import("../views/painterview.vue"),
    },
  ],
});
export default router;

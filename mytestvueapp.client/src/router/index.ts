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
      component: () => import("../views/PainterView.vue"),
    },
    {
      path: "/gallery",
      name: "Gallery View",
      component: () => import("../views/GalleryView.vue")
    }
  ],
});
export default router;

import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Landing",
      component: () => import("../views/LandingPageView.vue"),
    },
    {
      path: "/paint",
      name: "Painter View",
      component: () => import("../views/PainterView.vue"),
    },
    {
      path: "/new",
      name: "Prompt Painter",
      component: () => import("../views/PromptPainter.vue"),
    },
    {
      path: "/gallery",
      name: "Gallery View",
      component: () => import("../views/GalleryView.vue"),
    },
    {
      path: "/art/:id",
      name: "Image",
      component: () => import("../views/ImageViewerView.vue"),
    },
    {
      path: "/account",
      name: "Account",
      component: () => import("../views/AccountView.vue"),
    },
  ],
});
export default router;

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
      path: "/test",
      name: "HelloWorld",
      component: () => import("../components/HelloWorld.vue"),
    },
    {
      path: "/paint",
      name: "Painter View",
      component: () => import("../views/PainterView.vue"),
    },
    {
      path: "/prompt",
      name: "Prompt Painter",
      component: () => import("../views/PromptPainter.vue"),
    },
    {
      path: "/gallery",
      name: "Gallery View",
      component: () => import("../views/GalleryView.vue"),
      },
    {
      path: "/imgtest",
      name: "Image",
      component: () => import("../views/ImageViewer.vue"),
    },

  ],
});
export default router;

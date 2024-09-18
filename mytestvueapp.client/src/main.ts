import "./assets/main.css";

// Vue 3
import { createApp } from "vue";
import App from "./App.vue";

import router from "./router";

//PrimeVue
import PrimeVue from "primevue/config";
import "primeicons/primeicons.css";
import Aura from "@primevue/themes/aura";

//PrimeFlex
import "primeflex/primeflex.css";

createApp(App)
  .use(PrimeVue, {
    theme: {
      preset: Aura,
      options: {
        darkModeSelector: ".dark-mode-toggle",
      },
    },
  })
  .use(router)
  .mount("#app");

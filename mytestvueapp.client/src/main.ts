import "./assets/main.css";

// Vue 3
import { createApp } from "vue";
import App from "./App.vue";

import router from "./router";

//PrimeVue
import PrimeVue from "primevue/config";
import "primeicons/primeicons.css";
import Aura from "@primevue/themes/aura";
import ToastService from "primevue/toastservice";

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
  .use(ToastService)
  .mount("#app");

//Set the primary color of the theme
import { updatePreset } from "@primevue/themes";
updatePreset({
  semantic: {
    primary: {
      50: "#fdf2f8",
      100: "#fce7f3",
      200: "#fbcfe8",
      300: "#f9a8d4",
      400: "#f472b6",
      500: "#ec4899",
      600: "#db2777",
      700: "#be185d",
      800: "#9d174d",
      900: "#831843",
      950: "#500724",
    },
  },
  components: {
    card: {
      body: {
        padding: '0px',
        margin: '0px',
        gap: 0,
      },
      caption: {
        gap: '0px'
      }

    }
  }
});

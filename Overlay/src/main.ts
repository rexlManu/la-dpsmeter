import '@quasar/extras/roboto-font/roboto-font.css';
import '@quasar/extras/material-icons/material-icons.css';
import '@quasar/extras/fontawesome-v6/fontawesome-v6.css';
import '@quasar/extras/themify/themify.css';
import 'quasar/dist/quasar.css';

import { createPinia } from 'pinia';
import { Quasar } from 'quasar';
import { configure } from 'quasar/wrappers';
import { createApp } from 'vue';

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(
  Quasar,
  configure({
    plugins: {},
    config: {
      brand: {
        primary: "#1976d2",
        secondary: "#26A69A",
        accent: "#9C27B0",

        dark: "#1d1d1d",
        "dark-page": "#121212",

        positive: "#21BA45",
        negative: "#C10015",
        info: "#31CCEC",
        warning: "#F2C037",
      },
      dark: true,
    },
  })
);
app.use(createPinia());
app.use(router);

app.mount("#app");

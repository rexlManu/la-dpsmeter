import '@quasar/extras/roboto-font/roboto-font.css';
import '@quasar/extras/material-icons/material-icons.css';
import '@quasar/extras/fontawesome-v6/fontawesome-v6.css';
import '@quasar/extras/themify/themify.css';
import 'quasar/dist/quasar.css';

import { createPinia } from 'pinia';
import { Quasar } from 'quasar';
import { createApp } from 'vue';

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(Quasar, {
  plugins: {},
});
app.use(createPinia());
app.use(router);

app.mount("#app");

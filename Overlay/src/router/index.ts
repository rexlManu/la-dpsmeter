import { createRouter, createWebHashHistory } from 'vue-router';

import HomeView from '../views/DamageMeter.vue';

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
  ],
});

export default router;

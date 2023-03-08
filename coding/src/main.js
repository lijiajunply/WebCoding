import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'
import vueAxios from 'vue-axios'
createApp(App).use(vueAxios,axios).mount("#app");
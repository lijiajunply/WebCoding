import { createApp } from 'vue'
import App from './App.vue'
createApp(App).mount('#app')

import Axios from "axios"
import qs from 'qs';
window.qs = qs
window.axios=Axios

import './assets/main.css'

import store from './store'
import { createApp } from 'vue'
import App from './App.vue'
import Menu from '@/components/Menu.vue'

const app = createApp(App)
app.use(store)
app.component('my-menu', Menu)
app.mount('#app')

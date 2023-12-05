import './assets/main.css'

import store from './store'
import { createApp } from 'vue'
import App from './App.vue'
import Menu from '@/components/Menu.vue'
import UserList from '@/components/UserList.vue'

const app = createApp(App)
app.use(store)
app.component('my-menu', Menu)
app.component('user-list', UserList)
app.mount('#app')

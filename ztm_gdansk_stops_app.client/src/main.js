import './assets/main.css'

import store from './store'
import { createApp } from 'vue'
import App from './App.vue'
import Menu from '@/components/Menu.vue'
import UserList from '@/components/UserList.vue'
import StopsList from '@/components/StopsList.vue'
import StopInfo from '@/components/StopInfo.vue'
import UserStops from '@/components/UserStops.vue'
import LoginForm from '@/components/LoginForm.vue'
import AddUserForm from '@/components/AddUserForm.vue'
import InfoHeaderPlugin from "@/plugins/myplugin.js";
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'

const app = createApp(App)
app.use(store)
app.use(InfoHeaderPlugin)
app.use(Toast)
app.component('my-menu', Menu)
app.component('user-list', UserList)
app.component('stops-list', StopsList)
app.component('stop-info', StopInfo)
app.component('user-stops', UserStops)
app.component('login-form', LoginForm)
app.component('add-user-form', AddUserForm)
app.mount('#app')

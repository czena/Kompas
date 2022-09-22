import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import {CircleModule} from "@/store/circleModule";
import {AuthModule} from "@/store/authModule";
import {ErrorModule} from "@/store/errorModule";

createApp(App).use(store).use(router).mount('#app')

store.registerModule('circleModule', CircleModule);
store.registerModule('authModule', AuthModule);
store.registerModule('errorModule', ErrorModule);
import { createStore } from 'vuex';

const store = createStore({
    state() {
        return {
            loading: false,
            loggedIn: null,
            usersList: null,
            stops: null,
            stopInfo: null,
            addUserMode: false,
            loginMode: false,
            userStops: null,
            msg: null,
            isLightMode: false,
        }
    },
    mutations: {
        setAllDisplaysNull(state) {
            state.usersList = null;
            state.stops = null;
            state.stopInfo = null;
            state.addUserMode = false;
            state.loginMode = false;
            state.userStops = null;
            state.msg = null;
        },
        setLoading(state, value) {
            state.loading = value;
        },
        setLoggedIn(state, value) {
            state.loggedIn = value;
        },
        setUsersList(state, value) {
            state.usersList = value;
        },
        setStops(state, value) {
            state.stops = value;
        },
        setStopInfo(state, value) {
            state.stopInfo = value;
        },
        setAddUserMode(state, value) {
            state.addUserMode = value;
        },
        setLoginMode(state, value) {
            state.loginMode = value;
        },
        setUserStops(state, value) {
            state.userStops = value;
        },
        setMsg(state, value) {
            state.msg = value;
        },
        setIsLightMode(state, value) {
            state.isLightMode = value;
        },
    },
});

export default store;
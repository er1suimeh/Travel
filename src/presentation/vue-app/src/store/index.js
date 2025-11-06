import { createStore, createLogger } from "vuex";
import tourModule from "./tour";

const debug = process.env.NODE_ENV !== "production";

// Same idea as before — only use logger in development
const plugins = debug ? [createLogger()] : [];

export default createStore({
  state: {},
  getters: {},
  mutations: {},
  actions: {},
  modules: {
    tourModule,
  },
  plugins,
});

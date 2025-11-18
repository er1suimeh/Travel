import * as types from "./types";
import { jwtDecode } from "jwt-decode";

const mutations = {
  [types.LOGIN_USER](state, token) {
    state.signInState.token = token;
    const loginClaim = jwtDecode(token);
    claimToState(state, loginClaim);
    localStorage.setItem("token", token);
  },

  [types.LOCAL_STORAGE_TOKEN_LOG_IN](state, token) {
    state.signInState.token = token;
    const loginClaim = jwtDecode(token);
    claimToState(state, loginClaim);
  },
};

export default mutations;

function claimToState(state, claim) {
  state.signInState.sub = claim.sub;
  state.signInState.email = claim.email;
  state.signInState.exp = claim.exp;
}

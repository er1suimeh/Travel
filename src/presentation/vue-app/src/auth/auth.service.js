import api from "@/api/api-v1-config";

const key = "token";

export async function loginUserAxios(login) {
  return await api.post("v1.0/Users/auth", login);
}

export function getToken() {
  return localStorage.getItem(key);
}

export function logOut() {
  localStorage.clear();
  window.location = "/login";
}

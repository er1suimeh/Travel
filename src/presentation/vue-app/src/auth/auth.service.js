import api from "@/api/api-v1-config";
import { jwtDecode } from "jwt-decode";

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

export function isTokenFromLocalStorageValid() {
  const token = localStorage.getItem(key);
  if (!token) {
    return false;
  }

  const decoded = jwtDecode(token);
  const expiresAt = decoded.exp * 1000;
  const dateNow = Date.now();

  return dateNow <= expiresAt;
}

export function getUserEmailFromToken() {
  const token = localStorage.getItem(key);
  if (!token) return false;

  const decoded = jwtDecode(token);

  return decoded.email;
}

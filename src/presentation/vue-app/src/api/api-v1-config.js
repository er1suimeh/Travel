import axios from "axios";
import { interceptorsInit } from "@/api/interceptors";

const debug = process.env.NODE_ENV !== "production";
const baseURL = debug
  ? "https://localhost:7198/api/"
  : "https://traveltour.io/api/v1.0/";

let api = axios.create({ baseURL });
api = interceptorsInit(api);

export default api;

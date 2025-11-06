import axios from "axios";

const debug = process.env.NODE_ENV !== "production";
const baseURL = debug
  ? "https://localhost:7198/api/"
  : "https://traveltour.io/api/v1.0/";

let api = axios.create({ baseURL });

export default api;

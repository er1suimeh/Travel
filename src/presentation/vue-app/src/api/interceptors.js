import { getToken } from "@/auth/auth.service";

export function interceptorsInit(axiosInstance) {
  axiosInstance.interceptors.request.use((options) => {
    const jwtToken = getToken();

    if (jwtToken) {
      options.headers = {
        ...options.headers,
        Authorization: `Bearer ${jwtToken}`,
      };
    }

    return options;
  });

  axiosInstance.interceptors.response.use(
    (response) => response,
    (error) => {
      const message =
        (error.response && error.response.data) || "Something went wrong";

      return Promise.reject(message);
    }
  );

  return axiosInstance;
}

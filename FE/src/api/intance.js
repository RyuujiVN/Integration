import axios from "axios"
import { toast } from "react-toastify";

const instance = axios.create({
  baseURL: "http://localhost:5140/api",
  withCredentials: true,
  timeout: 10 * 60 * 1000
})

// Add a request interceptor
instance.interceptors.request.use(function (config) {
  // Do something before request is sent
  
  return config;
}, function (error) {
  // Do something with request error
  return Promise.reject(error);
});

// Add a response interceptor
instance.interceptors.response.use(function (response) {
  // Any status code that lie within the range of 2xx cause this function to trigger
  // Do something with response data
  return response;
}, function (error) {
  // Any status codes that falls outside the range of 2xx cause this function to trigger
  // Do something with response error

  if (error.response?.status !== 410) {
    toast.error(error.response?.data || error?.message);
  }
  return Promise.reject(error);
});

export default instance
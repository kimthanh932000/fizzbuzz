import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
  }
});

const get = async <T>(url: string): Promise<T> => {
  const res = await axiosInstance.get<T>(url);
  return res.data;
}

const post = async <T, P = void>(url: string, payload?: P): Promise<T> => {
  const res = await axiosInstance.post<T>(url, payload);
  return res.data;
}

export {axiosInstance, get, post};

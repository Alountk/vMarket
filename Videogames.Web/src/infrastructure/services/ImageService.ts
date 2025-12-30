import { axiosInstance } from "../api/axiosInstance";

export class ImageService {
  async uploadImage(file: File): Promise<string> {
    const formData = new FormData();
    formData.append("file", file);

    const response = await axiosInstance.post<{ fileName: string }>(
      "/Images/upload",
      formData,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );

    return response.data.fileName;
  }
}

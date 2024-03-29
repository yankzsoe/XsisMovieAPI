﻿namespace XsisMovieAPI.Application.Common.Models.Responses {
    public class Response<T> {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public Response() { }

        public Response(T data, string message) {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string errorMessage, List<string> errors) {
            Succeeded = false;
            Message = errorMessage;
            Errors = errors;
        }

        public Response(string message) {
            Succeeded = true;
            Message = message;
        }

    }
}

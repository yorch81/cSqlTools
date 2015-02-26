using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using System.IO;

namespace cSqlTools
{
    public class myStatic : NancyModule
    {
        /// <summary>
        /// Manage Static Content
        /// </summary>
        public myStatic()
        {
            Get["/static/bootstrap/css/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();
                               
                string content = myUtil.getTextForFile(cfg.CurrentDir + Request.Path);

                if (content == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.AsText(content, "text/plain");
            };

            Get["/static/bootstrap/js/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                string content = myUtil.getTextForFile(cfg.CurrentDir + Request.Path);

                if (content == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.AsText(content, "text/plain");
            };

            Get["/static/js/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                string content = myUtil.getTextForFile(cfg.CurrentDir + Request.Path);

                if (content == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.AsText(content, "text/plain");
            };

            Get["/static/jqfiletree/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                string content = myUtil.getTextForFile(cfg.CurrentDir + Request.Path);

                if (content == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.AsText(content, "text/plain");
            };

            Get["/static/img/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                FileStream stream = myUtil.getStreamForFile(cfg.CurrentDir + Request.Path);

                if (stream == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.FromStream(stream, "image/png");
            };

            Get["/static/jqfiletree/images/{file}"] = parameters =>
            {
                myConfig cfg = myConfig.getInstance();

                FileStream stream = myUtil.getStreamForFile(cfg.CurrentDir + Request.Path);

                if (stream == null)
                    return new Nancy.Response { StatusCode = Nancy.HttpStatusCode.NotFound };

                return Response.FromStream(stream, "image/png");
            };
        }
    }
}

using Nancy;
using System.IO;

/**
 * myStatic 
 *
 * myStatic Manage Static Content
 *
 * Copyright 2015 Jorge Alberto Ponce Turrubiates
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
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

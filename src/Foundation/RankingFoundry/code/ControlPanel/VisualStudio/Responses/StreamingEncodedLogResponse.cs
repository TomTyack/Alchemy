using System;
using System.IO;
using System.Web;
using Sitecore.Foundation.RankingFoundry.ControlPanel.Responses;
using Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Logging;

namespace Sitecore.Foundation.RankingFoundry.ControlPanel.VisualStudio.Responses
{
	public class StreamingEncodedLogResponse : IResponse
	{
		private readonly Action<IProgressStatus, ILogger> _processAction;

		public StreamingEncodedLogResponse(Action<IProgressStatus, ILogger> processAction)
		{
			_processAction = processAction;
		}

		public void Execute(HttpResponseBase response)
		{
			response.Buffer = false;
			response.BufferOutput = false;
			response.ContentType = "text/plain";

			using (var outputStream = response.OutputStream)
			{
				using (var streamWriter = new StreamWriter(outputStream))
				{
					var logger = new RemoteLogger(streamWriter);
					var progress = new RemoteProgressStatus(streamWriter);

					using (new UnicornOperationContext())
					{
						_processAction(progress, logger);
					}
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookworm_API.Models
{
	public class PagedResult<T>
	{
		IEnumerable<T> results { get; }
		int TotalRestults { get; }
		int PageNumber { get; }
	}
}
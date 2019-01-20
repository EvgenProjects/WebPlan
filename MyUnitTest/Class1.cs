using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Plan.Controllers;
using System.Web.Mvc;

namespace MyUnitTest
{
	[TestFixture]
	public class MyTest1
	{
		[Test]
		public void TestAuthenticationController()
		{
			// Arrange
			AuthenticationController controller = new AuthenticationController();

			// Act
			ViewResult result = controller.Login() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
		}
	}
}

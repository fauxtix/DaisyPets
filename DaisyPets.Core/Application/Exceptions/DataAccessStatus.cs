/*
 © Copyright 2018 Robert G Marquez - All Rights Reserved
 The source code contained within this file is intended for educational
 purposes only. No warranty as to the quality of the code is expressed or
 implied.
 
 THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE
   
  Feel free to use this code provided you include the copyright notice
  in its entirety.
*/
namespace DaisyPets.Core.Application.Exceptions
{
  public class DataAccessStatus
   {
      public string Status { get; set; }
      public bool OperationSucceeded { get; set; }
      public string ExceptionMessage { get; set; }
      public string CustomMessage { get; set; }
      public string HelpLink { get; set; }
      public int ErrorCode { get; set; }
      public string StackTrace { get; set; }


      public void setValues(string status, bool operationSucceeded, string exceptionMessage, string customMessage, string helpLink, int errorCode, string stackTrace)
    {
      Status = status ?? string.Copy("");
      OperationSucceeded = operationSucceeded;
      ExceptionMessage = exceptionMessage ?? string.Copy("");
      CustomMessage = customMessage ?? string.Copy("");
      HelpLink = helpLink ?? string.Copy("");
      ErrorCode = errorCode;
      StackTrace = stackTrace ?? string.Copy("");
    }
    public string getFormattedValues()
    {
      return $"Status--> {Status}\nOperatinSucceeded--> {OperationSucceeded}\nExceptinoMessage--> {ExceptionMessage}\nCustomMessage-->{CustomMessage}\nHelpLink--> {HelpLink}\nStackTrace--> {StackTrace}";
    }

  }
}

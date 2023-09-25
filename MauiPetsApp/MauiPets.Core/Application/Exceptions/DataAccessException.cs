/*
 © Copyright 2018 Robert G Marquez - All Rights Reserved
 The source code contained within this file is intended for educational
 purposes only. No warranty as to the quality of the code is expressed or
 implied.
 
 THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE
   
  Feel free to use this code provided you include the copyright notice
  in its entirety.
*/
using System;

namespace MauiPetsApp.Core.Application.Exceptions
{
    public class DataAccessException : Exception
  {
        public DataAccessException()
        {
        }
        private DataAccessStatus _dataAccessStatus { get; set; }


    public DataAccessStatus DataAccessStatusInfo
    {
      get { return _dataAccessStatus; }
      set { _dataAccessStatus = value; }
    }

    public DataAccessException(DataAccessStatus dataAccessStatus)
    {
      _dataAccessStatus = dataAccessStatus;
    }
    public DataAccessException(string message, Exception innerException, DataAccessStatus dataAccessStatus) : base(message, innerException)
    {
      _dataAccessStatus = dataAccessStatus;
    }


  }
}

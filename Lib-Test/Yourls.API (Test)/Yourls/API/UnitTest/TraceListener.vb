' NuGardt YOURLS API
' Copyright (C) 2013 NuGardt Software
' http://www.nugardt.com
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
'
Imports System.IO
Imports System.Text

Namespace Yourls.API.UnitTest
  ''' <summary>
  '''   This class write all trace output to a stream.
  ''' </summary>
  ''' <remarks></remarks>
  Public NotInheritable Class TraceListener
    Inherits Diagnostics.TraceListener

    ''' <summary>
    '''   Returns the stream for writing trace, or sets this.
    ''' </summary>
    ''' <remarks></remarks>
    Private Stream As Stream

    ''' <summary>
    '''   LogTime"><c>True</c> to logtime, otherwise <c>False</c>.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly LogTime As Boolean

    Private ReadOnly Relay As Action(Of String)

    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <param name="Stream">The stream for writing trace output.</param>
    ''' <param name="LogTime">
    '''   <c>True</c> to logtime, otherwise <c>False</c>.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Stream As Stream,
                   Optional ByVal Relay As Action(Of String) = Nothing,
                   Optional ByVal LogTime As Boolean = True)
      If Not Stream.CanWrite Then Throw New Exception("Stream not writable")

      Me.Stream = Stream
      Me.Relay = Relay
      Me.LogTime = LogTime

      Call Diagnostics.Trace.Listeners.Add(Me)

      Call Me.WriteLine("Log opened.")
    End Sub

    ''' <summary>
    '''   Construct.
    ''' </summary>
    ''' <param name="LogTime">
    '''   LogTime"><c>True</c> to logtime, otherwise <c>False</c>.
    ''' </param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal Relay As Action(Of String) = Nothing,
                   Optional ByVal LogTime As Boolean = True)
      Me.Relay = Relay
      Me.LogTime = LogTime

      Call Diagnostics.Trace.Listeners.Add(Me)

      Call Me.WriteLine("Log opened.")
    End Sub

    Protected Overrides Sub Dispose(ByVal Disposing As Boolean)
      Call Diagnostics.Trace.Listeners.Remove(Me)

      Call Me.WriteLine("Log closed.")

      If Me.Stream IsNot Nothing Then
        With Me.Stream
          Call .Flush()
          Call .Close()
          Call .Dispose()
        End With
      End If
      Me.Stream = Nothing

      Call MyBase.Dispose(Disposing)
    End Sub

    ''' <summary>
    '''   Write information of the system into the log file.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub WriteSystemInformation()
      Dim LoadedAssemblies As New StringBuilder

      Try
        With My.Application.Info.LoadedAssemblies.GetEnumerator
          Call .Reset()

          Do While .MoveNext
            Call LoadedAssemblies.AppendLine(.Current.FullName)
          Loop

          Call .Dispose()
        End With
      Catch iEx As Exception
        Call Trace.WriteLine("Unable to query all assemblies: " + iEx.ToString())
      End Try

      Try
        Call Me.WriteLine(String.Format("Physical memory: {0}/{1}bytes, Virtual memory: {2}/{3}bytes, OS: {4}, Platform: {5}, Version: {6}, User name: {7}, Computer Name: {8}, Culture: {9}", My.Computer.Info.AvailablePhysicalMemory.ToString("N0"), My.Computer.Info.TotalPhysicalMemory.ToString("N0"), My.Computer.Info.AvailableVirtualMemory.ToString("N0"), My.Computer.Info.TotalVirtualMemory.ToString("N0"), My.Computer.Info.OSFullName, My.Computer.Info.OSPlatform, My.Computer.Info.OSVersion, My.User.Name, My.Computer.Name, My.Computer.Info.InstalledUICulture.ToString))
      Catch iEx As Exception
        Call Trace.WriteLine("Unable to query system: " + iEx.ToString())
      End Try

      Try
        Call Me.WriteLine(String.Format("Product Name: {0}, Assembly Name: {1}, Version: {2}, Loaded Assemblies: {3}", My.Application.Info.ProductName, My.Application.Info.AssemblyName, My.Application.Info.Version, LoadedAssemblies.ToString))
      Catch iEx As Exception
        Call Trace.WriteLine("Unable to query product details: " + iEx.ToString())
      End Try
    End Sub

    ''' <summary>
    '''   Write data to stream.
    ''' </summary>
    ''' <param name="Data">
    '''   The data to be
    '''   written.
    ''' </param>
    ''' <remarks></remarks>
    Private Overloads Sub Write(ByVal Data() As Byte)
      If Data IsNot Nothing Then
        If Me.Stream IsNot Nothing Then
          SyncLock Me.Stream
            Call Me.Stream.Write(Data, 0, Data.Length)
          End SyncLock
        End If
      End If
    End Sub

    ''' <summary>
    '''   Write a message into stream.
    ''' </summary>
    ''' <param name="Message">The message to be written.</param>
    ''' <remarks></remarks>
    Public Overloads Overrides Sub Write(ByVal Message As String)
      If Me.LogTime Then
        Message = String.Format("{0}: {1}", Now.ToString("yyyy-MM-dd HH:mm:ss"), Message)
        Call Me.Write(Encoding.UTF8.GetBytes(Message))
      Else
        Call Me.Write(Encoding.UTF8.GetBytes(Message))
      End If

      If Me.Relay IsNot Nothing Then Me.Relay.Invoke(Message)
    End Sub

    ''' <summary>
    '''   Write a message line into stream.
    ''' </summary>
    ''' <param name="Message">The message to be written.</param>
    ''' <remarks></remarks>
    Public Overloads Overrides Sub WriteLine(ByVal Message As String)
      If Me.LogTime Then
        Message = String.Format("{0}: {1}", Now.ToString("yyyy-MM-dd HH:mm:ss"), Message + vbCrLf)
        Call Me.Write(Encoding.UTF8.GetBytes(Message))
      Else
        Call Me.Write(Encoding.UTF8.GetBytes(Message + vbCrLf))
      End If

      If Me.Relay IsNot Nothing Then Me.Relay.Invoke(Message)
    End Sub
  End Class
End Namespace
#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;

namespace Scripts.Test
{
    public class TestSocketIO : MonoBehaviour
    {
        [SerializeField] private InputField roomField;
        [SerializeField] private InputField textField;
        [SerializeField] private Text window;
        [SerializeField] private Text msgwindow;
    
        private SocketIOComponent socket;
    
        private void Start()
        {
            socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
            socket.On("roomresp", OnConnect);
            socket.On("message", OnMessage);
        }

        public void SendRoom()
        {
            socket.Emit("room", JSONObject.CreateStringObject(roomField.text));
        }

        public void SendMess()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("room", roomField.text);
            dict.Add("mess", textField.text);
            socket.Emit("data", JSONObject.Create(dict));
        }

        private void OnConnect(SocketIOEvent e)
        {
            window.text = $"H:{e.name} : D:{e.data.GetField("connect").str}";
        }
        private void OnMessage(SocketIOEvent e)
        {
            msgwindow.text += $"H:{e.name} : D:{e.data.GetField("message").str}\n";
            Debug.Log(e.data.GetField("my").str);
        }
    }
}



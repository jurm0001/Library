using System;
using System.Collections.Generic;
using System.Text;

using IBM.WMQ;


namespace Utilities
{
    public class BaseMQ
    {

        protected MQQueueManager queueManager;
        protected MQQueue queue;
        protected MQMessage queueMessage;
        protected MQPutMessageOptions queuePutMessageOptions;
        protected MQGetMessageOptions queueGetMessageOptions;

        static string QueueName;
        static string QueueManagerName;
        static string ChannelInfo;
        string channelName;
        string transportType;
        string connectionName;
        string message;

        public BaseMQ()
        {
            //Initialisation - SAMPLE
            /* 
            QueueManagerName = "QM_TEST";
            QueueName = " QM_TEST.LOCAL.ONE"; 
            ChannelInfo = "QM._TEST.SVRCONN/TCP/psingh(1421)";
            */
        }

        public string Connect(string strQueueManagerName,
                              string strQueueName,
                              string strChannelInfo)
        {
            QueueManagerName = strQueueManagerName;
            QueueName = strQueueName;
            ChannelInfo = strChannelInfo;
            //
            char[] separator = { '/' };
            string[] ChannelParams;
            ChannelParams = ChannelInfo.Split(separator);
            channelName = ChannelParams[0];
            transportType = ChannelParams[1];
            connectionName = ChannelParams[2];
            String strReturn = "";
            try
            {
                queueManager = new MQQueueManager(QueueManagerName,
                channelName, connectionName);
                strReturn = "Connected Successfully";
            }
            catch (MQException exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public int Count()
        {
            int count = 0;
            try
            {                
                queue = queueManager.AccessQueue(QueueName, MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_BROWSE);
                queueMessage = new MQMessage();
                //queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queueGetMessageOptions.Options = MQC.MQGMO_FAIL_IF_QUIESCING + MQC.MQGMO_NO_WAIT + MQC.MQGMO_BROWSE_NEXT;

                while (true)
                {
                    queue.Get(queueMessage, queueGetMessageOptions);
                    ++count;                    
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return count;
        }

        public string peekTopMessage()
        {
            string strReturn = "";
            try
            {

                queue = queueManager.AccessQueue(QueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_BROWSE);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queueGetMessageOptions.Options = MQC.MQGMO_BROWSE_FIRST;
                queue.Get(queueMessage, queueGetMessageOptions);

                strReturn =
                queueMessage.ReadString(queueMessage.MessageLength);
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception : " + MQexp.Message;
                return "";
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                return "";
            }
            return strReturn;
        }


        public Boolean isMessageQueued()
        {
            string strReturn = "";
            try
            {
                
                queue = queueManager.AccessQueue(QueueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING + MQC.MQOO_BROWSE);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queueGetMessageOptions.Options = MQC.MQGMO_BROWSE_FIRST;
                queue.Get(queueMessage, queueGetMessageOptions);

                strReturn =
                queueMessage.ReadString(queueMessage.MessageLength);
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception : " + MQexp.Message;
                return false;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
                return false;
            }
            return true;
        }

        public virtual string Get()
        {
            string strReturn = "";
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                
                queue.Get(queueMessage, queueGetMessageOptions);
                
                strReturn = queueMessage.ReadString(queueMessage.MessageLength);                
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception : " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public virtual string Get(int waitInterval)
        {
            string strReturn = "";
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queueGetMessageOptions.WaitInterval = waitInterval;
                queue.Get(queueMessage, queueGetMessageOptions);

                strReturn = queueMessage.ReadString(queueMessage.MessageLength);
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception : " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public virtual MQMessage GetMessage()
        {            
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING);
                queueMessage = new MQMessage();
                queueMessage.Format = MQC.MQFMT_STRING;
                queueGetMessageOptions = new MQGetMessageOptions();
                queue.Get(queueMessage, queueGetMessageOptions);
                //strReturn = queueMessage.ReadString(queueMessage.MessageLength);
            }
            catch (MQException MQexp)
            {
                throw MQexp;
                //strReturn = "Exception : " + MQexp.Message;
            }
            catch (Exception exp)
            {
                //strReturn = "Exception: " + exp.Message;
                throw exp;
            } 
            return queueMessage;
        }


        public virtual string Put(string strInputMsg)
        {
            string strReturn = "";
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);
                message = strInputMsg;
                queueMessage = new MQMessage();
                queueMessage.WriteString(message);
                queueMessage.Format = MQC.MQFMT_STRING;
                queuePutMessageOptions = new MQPutMessageOptions();

                //queueMessage.Persistence = MQC.MQPER_NOT_PERSISTENT;

                queue.Put(queueMessage, queuePutMessageOptions);
                strReturn = "SUCCESS";
            }

            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public virtual string Put(string strInputMsg, bool persistent)
        {
            string strReturn = "";
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);
                message = strInputMsg;
                queueMessage = new MQMessage();
                queueMessage.WriteString(message);
                queueMessage.Format = MQC.MQFMT_STRING;
                queuePutMessageOptions = new MQPutMessageOptions();

                if(!persistent)
                    queueMessage.Persistence = MQC.MQPER_NOT_PERSISTENT;

                queue.Put(queueMessage, queuePutMessageOptions);
                strReturn = "SUCCESS";
            }

            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public virtual string Put(MQMessage InputMsg, MQPutMessageOptions queuePutMessageOptions)
        {
            string strReturn = "";
            try
            {
                queue = queueManager.AccessQueue(QueueName,
                MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);                   

                queue.Put(InputMsg, queuePutMessageOptions);
                strReturn = "SUCCESS";
            }

            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }        
    }
}
